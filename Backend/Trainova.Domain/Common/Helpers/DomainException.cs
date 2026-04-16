namespace Trainova.Domain.Common.Helpers;

public class DomainException :Exception
{
    public DomainException(string message = "Domain Error Happend", string code = "DomainError"): base(message)
    {
        Code = code;
    }
    public string Code { get; internal set; }

}
