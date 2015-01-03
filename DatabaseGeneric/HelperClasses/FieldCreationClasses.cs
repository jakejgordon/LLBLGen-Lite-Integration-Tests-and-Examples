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
using SD.LLBLGen.Pro.ORMSupportClasses;
using LLBLGenLiteExamples.FactoryClasses;
using LLBLGenLiteExamples;

namespace LLBLGenLiteExamples.HelperClasses
{
	/// <summary>Field Creation Class for entity PersonEntity</summary>
	public partial class PersonFields
	{
		/// <summary>Creates a new PersonEntity.Id field instance</summary>
		public static EntityField2 Id
		{
			get { return (EntityField2)EntityFieldFactory.Create(PersonFieldIndex.Id);}
		}
		/// <summary>Creates a new PersonEntity.Name field instance</summary>
		public static EntityField2 Name
		{
			get { return (EntityField2)EntityFieldFactory.Create(PersonFieldIndex.Name);}
		}
	}

	/// <summary>Field Creation Class for entity PetEntity</summary>
	public partial class PetFields
	{
		/// <summary>Creates a new PetEntity.FavoritePetFoodBrandId field instance</summary>
		public static EntityField2 FavoritePetFoodBrandId
		{
			get { return (EntityField2)EntityFieldFactory.Create(PetFieldIndex.FavoritePetFoodBrandId);}
		}
		/// <summary>Creates a new PetEntity.Id field instance</summary>
		public static EntityField2 Id
		{
			get { return (EntityField2)EntityFieldFactory.Create(PetFieldIndex.Id);}
		}
		/// <summary>Creates a new PetEntity.Name field instance</summary>
		public static EntityField2 Name
		{
			get { return (EntityField2)EntityFieldFactory.Create(PetFieldIndex.Name);}
		}
		/// <summary>Creates a new PetEntity.OwningPersonId field instance</summary>
		public static EntityField2 OwningPersonId
		{
			get { return (EntityField2)EntityFieldFactory.Create(PetFieldIndex.OwningPersonId);}
		}
	}

	/// <summary>Field Creation Class for entity PetFoodBrandEntity</summary>
	public partial class PetFoodBrandFields
	{
		/// <summary>Creates a new PetFoodBrandEntity.BrandName field instance</summary>
		public static EntityField2 BrandName
		{
			get { return (EntityField2)EntityFieldFactory.Create(PetFoodBrandFieldIndex.BrandName);}
		}
		/// <summary>Creates a new PetFoodBrandEntity.Id field instance</summary>
		public static EntityField2 Id
		{
			get { return (EntityField2)EntityFieldFactory.Create(PetFoodBrandFieldIndex.Id);}
		}
	}
	

}