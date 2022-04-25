namespace BatailleNavale
{
	public class Player
	{
		
		public string? Name { get; set; }

		public int[] SizesBoats = { 2, 3, 3, 4, 5 };
		public List<Boat> ListOfBoats { get; set; } = new();
		public Cell[][] Grid { get; set; } = new Cell[10][];

		//

		public void InitGrid()
		{
			//
			for (int i = 0; i < 10; i++)
			{
				Grid[i] = new Cell[10];
			}

			//
			for (int x = 0; x < 10; x++)
			{
				for (int y = 0; y < 10; y++)
				{
					Grid[x][y] = new Cell() { CoordX = x, CoordY = y, AlreadyPlayed = false, NumBoat = -1 };
				}
			}
		}

		public void InitBoat()
		{
			for (int i = 0; i < SizesBoats.Length; i++)
			{
				Boat boat = new(SizesBoats[i], i);
				ListOfBoats.Add(boat);
			}
		}


		////////////////////////METHODES TEST ////////////////////////////////////////////
		public void ShowBoats()
		{
			foreach (Boat boat in ListOfBoats)
			{
				Console.WriteLine(boat.Size + " " + boat.Id+" "+boat.x0+" "+boat.y0+" "+boat.Orientation);
			}
		}

		public void ShowGridInfos()
		{

			for (int y = 0; y < 10; y++)
			{
				for (int x = 0; x < 10; x++)
				{
					Console.WriteLine("({0},{1})", Grid[x][y].NumBoat, "");
				}
			}
		}


	}
}
