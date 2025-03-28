using Godot;

public partial class DataTypes : Node
{
	public enum Tools {
		None,
		AxeWood,	// 砍树
		TillGround,	// 耕地
		WaterCrops,	// 浇水
		PlantCorn,	
		Plantomato
	}

	public enum GrowthStates {
		Seed,
		Germination,	// 萌芽
		Vegetative,		// 生长
		Reproduction, 	// 繁殖
		Maturity,		// 成熟
		Harvesting		// 收获
	}

}
