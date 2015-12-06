// First Chest version: 1.2.8
// Author: Gold Experience Team (http://www.ge-team.com/pages/)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces
using UnityEngine;
using System.Collections;

#endregion

/***************
* First Chest Prop Particle Handler.
* This class derived form FCParticle class. It contains a function to move particle follow the Prop.
***************/

public class FCPropParticle : FCParticle
{

  #region Variables

		// Variables
		public bool m_CreateWhenChestOpen = true;
		public bool m_RemovedWhenChestClose = true;

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
				// Move PropParticle position to follow the Prop
				if (getParticleGameObject () != null) {
						if (getMain () != null) {
								if (getMain ().getProp ().getPropGameObject () != null) {
										getParticleGameObject ().transform.position = getMain ().getProp ().getPropGameObject ().transform.position + m_Prefab.transform.position + m_OffSetLocalPosition;
								}
						}
				}
		}

  #endregion
}
