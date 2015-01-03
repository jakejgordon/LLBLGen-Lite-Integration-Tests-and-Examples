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
	/// <summary>Implements the relations factory for the entity: Pet. </summary>
	public partial class PetRelations
	{
		/// <summary>CTor</summary>
		public PetRelations()
		{
		}

		/// <summary>Gets all relations of the PetEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.PersonEntityUsingOwningPersonId);
			toReturn.Add(this.PetFoodBrandEntityUsingFavoritePetFoodBrandId);
			return toReturn;
		}

		#region Class Property Declarations



		/// <summary>Returns a new IEntityRelation object, between PetEntity and PersonEntity over the m:1 relation they have, using the relation between the fields:
		/// Pet.OwningPersonId - Person.Id
		/// </summary>
		public virtual IEntityRelation PersonEntityUsingOwningPersonId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Person", false);
				relation.AddEntityFieldPair(PersonFields.Id, PetFields.OwningPersonId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PersonEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PetEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between PetEntity and PetFoodBrandEntity over the m:1 relation they have, using the relation between the fields:
		/// Pet.FavoritePetFoodBrandId - PetFoodBrand.Id
		/// </summary>
		public virtual IEntityRelation PetFoodBrandEntityUsingFavoritePetFoodBrandId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "PetFoodBrand", false);
				relation.AddEntityFieldPair(PetFoodBrandFields.Id, PetFields.FavoritePetFoodBrandId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PetFoodBrandEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PetEntity", true);
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
	internal static class StaticPetRelations
	{
		internal static readonly IEntityRelation PersonEntityUsingOwningPersonIdStatic = new PetRelations().PersonEntityUsingOwningPersonId;
		internal static readonly IEntityRelation PetFoodBrandEntityUsingFavoritePetFoodBrandIdStatic = new PetRelations().PetFoodBrandEntityUsingFavoritePetFoodBrandId;

		/// <summary>CTor</summary>
		static StaticPetRelations()
		{
		}
	}
}
