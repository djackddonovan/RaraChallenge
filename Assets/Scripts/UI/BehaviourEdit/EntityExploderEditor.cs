using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityExploderEditor : BehaviourEditor
{

	public override Type BehaviourType { get { return typeof(EntityExploderDefinition); } }
	public override string BehaviourName { get { return "Exploder"; } }

}
