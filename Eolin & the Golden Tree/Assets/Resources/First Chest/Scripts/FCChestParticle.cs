// First Chest version: 1.2.8
// Author: Gold Experience Team (http://www.ge-team.com/pages/)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces
using UnityEngine;
using System.Collections;

#endregion

/***************
* First Chest Chest Particle Handler.
* This class derived form FCParticle class. It contains a function to move particle follow the Chest.
***************/

public class FCChestParticle : FCParticle
{
  #region Variables

		// Variables
		public bool m_RemoveWhenChestOpen = true;
		public bool m_CreateWhenChestClose = false;

  #endregion

		// ######################################################################
		// MonoBehaviour Functions
		// ######################################################################

  #region Component Segments

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{
				// Move Particle when user updates ChestParticle Offset position in Inspector
				UpdateParticlePosition ();
		}

  #endregion
}
