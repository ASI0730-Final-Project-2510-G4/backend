namespace CreatiLinkPlatform.Contract.Interfaces.REST.Resources;

public record UpdateContractResource(
    decimal Price,
    string Requirements,
    string DesignType
);