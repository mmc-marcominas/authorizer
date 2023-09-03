using System.Runtime.Serialization;

namespace authorizer.Infra;

[Serializable]
internal class RequiredDataNotFoundException : Exception
{
  public RequiredDataNotFoundException() : base("Required data not found")
  {
  }

  public RequiredDataNotFoundException(string message) : base(message)
  {
  }

  public RequiredDataNotFoundException(string message, Exception innerException) : base(message, innerException)
  {
  }

  protected RequiredDataNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
  {
  }
}
