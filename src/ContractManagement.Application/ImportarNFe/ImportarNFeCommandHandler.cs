using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity.Catalogo;
using ContractManagement.Domain.Entity.Estoques;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Shared;



namespace ContractManagement.Application.ImportarNFe
{
    internal sealed class ImportarNFeCommandHandler(INotaEntradaRepository notaEntradaRepository, IProdutoRepository produtoRepository, IUnitOfWork unitOfWork) : ICommandHandler<ImportarNFeCommand, Guid>
    {
        private readonly INotaEntradaRepository _notaEntradaRepository = notaEntradaRepository;
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result<Guid>> Handle(ImportarNFeCommand request, CancellationToken cancellationToken)
        {
            var nfeProc = ExtNfeProc.CarregarDeXmlString(null, request.XmlContent);

                       

            if (nfeProc is null)
            {
                return Result.Failure<Guid>(new Error("xml_invalid", "O XML informado não contém uma NFe válida."));
            }

            var infNFe = nfeProc.NFe.infNFe;

            if (infNFe.det is null || infNFe.det.Count == 0)
            {
                return Result.Failure<Guid>(new Error("xml_semItens", "NF-e sem itens."));
            }

            var itensData = new List<ItemNotaEntradaData>();

            foreach (var item in infNFe.det)
            {
                var prod = item.prod;

                if (prod is null)
                {
                    return Result.Failure<Guid>(new Error("item_invalido", "Item da Nf-e invalido"));
                }

                var produto = await _produtoRepository.GetByCodigoAsync(prod.cProd, cancellationToken);

                if (produto is null)
                {
                    produto = Produto.Create(
                        nome: prod.xProd,
                        imagem: null,
                        unidadeMedida: prod.uCom,
                        codigoBarras: prod.cEAN,
                        observacao: "",
                        codigo: prod.cProd,
                        precoVenda: 0,
                        precoCusto: prod.vUnCom,
                        estoqueAtual: 0,
                        estoqueMaximo: 0,
                        estoqueMinino: 0,
                        ativo: true
                    );

                    await _produtoRepository.CreateProduto(produto, cancellationToken);
                }

                itensData.Add(new ItemNotaEntradaData(produto.Id, (int)item.prod.qCom, item.prod.vUnCom));
            }

            

            var notaEntrada = NotaEntrada.Create(infNFe.ide.nNF.ToString(), itensData);

            var notaEntradaExistente = await _notaEntradaRepository.ObterNotaEntradaPorNumeroDocumento(notaEntrada.NumeroDocumento, cancellationToken);

            if (notaEntradaExistente is not null)
            {
                return Result.Failure<Guid>(new Error("notaEntradaJáCadastrada", "A Nota que você quer importar já se encontrada cadastrada"));
            }

            await _notaEntradaRepository.Adicionar(notaEntrada, cancellationToken);

            await _unitOfWork.Commit(cancellationToken);

            return Result.Create(notaEntrada.Id);
        }
    }
}
