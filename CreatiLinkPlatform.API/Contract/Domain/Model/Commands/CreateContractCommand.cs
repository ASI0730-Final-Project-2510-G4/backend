namespace CreatiLinkPlatform.Contract.Domain.Model.Commands;

public record CreateContractCommand(
    int ClientUserId,
    int DesignerProfileId,
    decimal Price,
    string Requirements,
    string DesignType
    );