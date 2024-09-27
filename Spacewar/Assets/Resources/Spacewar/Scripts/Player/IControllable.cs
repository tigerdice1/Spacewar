public interface IControllable{
    void Move(PlayerController controller);
    void Look(PlayerController controller, float maxRotationSpeed, bool useSlerp);
    void HandleMouseClick(PlayerController controller);
    void UpdateAnimation(PlayerController controller);
    float RotationSpeed { get; }
}