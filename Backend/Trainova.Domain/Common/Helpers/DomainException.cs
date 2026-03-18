namespace Trainova.Domain.Common.Helpers;

public class DomainException :Exception
{
    public DomainException(string massage,string code = "DomainError"): base(massage)
    {
        Code = code;
    }
    public string Code { get; internal set; }

}
