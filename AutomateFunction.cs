using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Objects;
using Objects.BuiltElements;
using Objects.Geometry;
using Objects.Other;
using Speckle.Core.Api;
using Speckle.Core.Credentials;
using Speckle.Core.Models;
using Speckle.Core.Models.Extensions;
using Speckle.Core.Models.GraphTraversal;
using Speckle.Core.Transports;
using SpeckleAutomateDotnetExample;
using SpeckleAutomateDotnetExample.Rules;

/// <summary>
/// This class describes the user specified variables that the function wants to work with.
/// </summary>
/// This class is used to generate a JSON Schema to ensure that the user provided values
/// are valid and match the required schema.
class FunctionInputs
{

}

class AutomateFunction
{
  public static async Task<int> Run(
    SpeckleProjectData speckleProjectData,
    FunctionInputs functionInputs,
    string speckleToken
  )
  {
    var account = new Account
    {
      token = speckleToken,
      serverInfo = new ServerInfo() { url = speckleProjectData.SpeckleServerUrl }
    };
    var client = new Client(account);


    // HACK needed for the objects kit to initialize
    var p = new Point();

    var commit = await client.CommitGet(
      speckleProjectData.ProjectId,
      speckleProjectData.VersionId
    );

    var serverTransport = new ServerTransport(account, speckleProjectData.ProjectId);
    var rootObject = await Operations.Receive(
      commit.referencedObject,
      serverTransport,
      new MemoryTransport()
    );
    if (rootObject is null) throw new Exception("root object is null");

    //return rootObject.Flatten().Count( b => b.speckle_type == functionInputs.SpeckleTypeToCount);
    var rules = CreateRules();
    ValidateCommit(rootObject, rules);
    return 0;
  }

  private static void ValidateCommit(Base root, IList<GrammarRule> rules)
  {
    var objects = DefaultTraversal.CreateTraverseFunc(new DummyConverter());

    foreach (var t in objects.Traverse(root))
    {
      Base current = t.current;
      var activeRule = rules.FirstOrDefault(r => r.DoesApply(current));
      if (activeRule is null) continue;
      var children = GraphTraversal.TraverseMember(current["elements"] ?? current["@elements"]);
      foreach (var child in children)
      {
        if (!activeRule.IsValidChild(child))
        {
          Console.WriteLine($"{child.speckle_type} {child.id} is not a valid child of {current.speckle_type} {current.id} according to rule: {activeRule}");
        }
      }
    }
  }
  
  private static IList<GrammarRule> CreateRules()
  {
    return new GrammarRule[] { new CollectionRule(), new InstanceRule(), new DisplayValueRule(), new RawGeometryRule() };
  }
}

