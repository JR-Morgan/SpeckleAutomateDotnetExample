using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Speckle.Automate.Sdk.DataAnnotations;

/// <summary>
/// This class describes the user specified variables that the function wants to work with.
/// </summary>
/// This class is used to generate a JSON Schema to ensure that the user provided values
/// are valid and match the required schema.
public readonly struct FunctionInputs
{
  /// <summary>
  /// The object type to count instances of in the given model version.
  /// </summary>
  [Required]
  public string SpeckleTypeToCount { get; init; }

  /// <summary>
  /// The total number of the specified type expected.
  /// </summary>
  [DefaultValue(10)]
  [Range(1, 100)]
  [Required]
  public int SpeckleTypeTargetCount { get; init; }

  /// <summary>
  /// An arbitrary example of using a secret input value.
  /// </summary>
  [Required]
  [Secret]
  public string ExternalServiceKey { get; init; }
}
