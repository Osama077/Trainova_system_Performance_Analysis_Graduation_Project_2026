namespace Trainova.Common.ResultOf;

public record struct Done
{
    string Massage = "The action has been Doneed";

    public Done(string massage="")
    {
        Massage = massage;
    }
    public static Done done => new Done();
    public static Done NoContent => new Done("The action has been Doneed with no content");
}
