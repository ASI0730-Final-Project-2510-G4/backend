namespace CreatiLinkPlatform.Contract.Domain.Model.Commands;

public record DeleteContractCommand(
    int ContractId,
    decimal Price,
    string Requirements,
    string DesignType
    
    );