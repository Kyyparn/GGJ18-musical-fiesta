public class ElevatorButton : Interactable {
    
    public Elevator Elevator;

    // Use this for initialization
    public override void Interract()
    {
        if(Elevator.IsElevatorOpen)
        {
            Elevator.CloseDoors();
            Invoke("LoadNextLevel", 5);
        }
    }

    private void LoadNextLevel()
    {
        Elevator.LoadNextLevel();
    }
}
