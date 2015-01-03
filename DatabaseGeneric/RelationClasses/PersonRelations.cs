///////////////////////////////////////////////////////////////
// This is generated code. 
//////////////////////////////////////////////////////////////
// Code is generated using LLBLGen Pro version: 4.2
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates
// Templates vendor: Solutions Design.
// Templates version: 
//////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using LLBLGenLiteExamples;
using LLBLGenLiteExamples.FactoryClasses;
using LLBLGenLiteExamples.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace LLBLGenLiteExamples.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Person. </summary>
	public partial class PersonRelations
	{
		/// <summary>CTor</summary>
		public PersonRelations()
		{
		}

		/// <summary>Gets all relations of the PersonEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.PetEntityUsingOwningPersonId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between PersonEntity and PetEntity over the 1:n relation they have, using the relation between the fields:
		/// Person.Id - Pet.OwningPersonId
		/// </summary>
		public virtual IEntityRelation PetEntityUsingOwningPersonId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Pets" , true);
				relation.AddEntityFieldPair(PersonFields.Id, PetFields.OwningPersonId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PersonEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PetEntity", false);
				return relation;
			}
		}


		/// <summary>stub, not used in this entity, only for TargetPerEntity entities.</summary>
		public virtual IEntityRelation GetSubTypeRelation(string subTypeEntityName) { return null; }
		/// <summary>stub, not used in this entity, only for TargetPerEntity entities.</summary>
		public virtual IEntityRelation GetSuperTypeRelation() { return null;}
		#endregion

		#region Included Code

		#endregion
	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticPersonRelations
	{
		internal static readonly IEntityRelation PetEntityUsingOwningPersonIdStatic = new PersonRelations().PetEntityUsingOwningPersonId;

		/// <summary>CTor</summary>
		static StaticPersonRelations()
		{
		}
	}
}
