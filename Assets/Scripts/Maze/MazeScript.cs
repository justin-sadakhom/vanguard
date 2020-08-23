using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeScript : MonoBehaviour {

	public IntVector2 size;
	public MazeCells cellPrefab;
	public MazePassage passagePrefab;
	public MazeWall wallPrefab;

	private MazeCells[,] cells;

	public MazeCells GetCell (IntVector2 coordinates) {
		return cells[coordinates.x, coordinates.z];
	}

	public float generationStepDelay;

	public IntVector2 RandomCoordinates {
		get {
			return new IntVector2 (Random.Range (0, size.x), Random.Range (0, size.z));
		}
	}

	public bool ContainsCoordinates(IntVector2 coordinate) {
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}

	public IEnumerator Generate() {
		WaitForSeconds delay = new WaitForSeconds(generationStepDelay);

		cells = new MazeCells[size.x, size.z];

		List<MazeCells> activeCells = new List<MazeCells>();

		DoFirstGenerationStep(activeCells);

		while (activeCells.Count > 0) {
			yield return delay;
			DoNextGenerationStep(activeCells);
		}
	}

	private MazeCells CreateCell(IntVector2 coordinates) {
		MazeCells newCell = Instantiate(cellPrefab) as MazeCells;
		cells[coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition =
			new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}

	private void DoFirstGenerationStep(List<MazeCells> activeCells) {
		activeCells.Add(CreateCell(RandomCoordinates));
	}

	private void DoNextGenerationStep(List<MazeCells> activeCells) {

		int currentIndex = activeCells.Count - 1;
		MazeCells currentCell = activeCells[currentIndex];
		MazeDirection direction = MazeDirections.RandomValue;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();

		if (ContainsCoordinates(coordinates)) {

			MazeCells neighbor = GetCell(coordinates);

			if (neighbor == null) {

				neighbor = CreateCell(coordinates);

				CreatePassage(currentCell, neighbor, direction);

				activeCells.Add(neighbor);
			}
			else {
				CreateWall(currentCell, neighbor, direction);
				activeCells.RemoveAt(currentIndex);
			}
		}
		else {
			CreateWall(currentCell, null, direction);
			activeCells.RemoveAt(currentIndex);
		}
	}

	private void CreatePassage(MazeCells cell, MazeCells otherCell, MazeDirection direction) {

		MazePassage passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}

	private void CreateWall(MazeCells cell, MazeCells otherCell, MazeDirection direction) {

		MazeWall wall = Instantiate(wallPrefab) as MazeWall;

		wall.Initialize(cell, otherCell, direction);

		if (otherCell != null) {
			wall = Instantiate(wallPrefab) as MazeWall;
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		}
	}
}
