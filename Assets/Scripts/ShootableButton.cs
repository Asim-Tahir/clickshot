using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Renderer))]
public class ShootableButton : MonoBehaviour
{
    private int _pressHash;
    private Animator _animator;
    private Renderer _renderer;

    public bool IsPressed { get; private set; } = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<Renderer>();

        _pressHash = Animator.StringToHash("Press");
    }

    public void Press()
    {
        IsPressed = true;

        RandomizeColor();

        _animator.SetTrigger(_pressHash);
    }

    public void RandomizeColor()
    {
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}
