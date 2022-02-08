using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAgent : Agent
{
    [SerializeField] Perception perception;

    public PathFollower pathFollower;
    public StateMachine stateMachine = new StateMachine();

    public BoolRef enemySeen;
    public IntRef health;
    public FloatRef timer;

    public GameObject enemy { get; set; }

    void Start()
    {
        stateMachine.AddState(new IdleState(this, "idle"));
        stateMachine.AddState(new PatrolState(this, "patrol"));
        stateMachine.AddState(new ChaseState(this, "chase"));

        stateMachine.AddTransition("idle", new BoolTransition(enemySeen, true), "chase");
        stateMachine.AddTransition("idle", new FloatTransition(timer, Transition.Predicate.LESS, 0), "patrol");
        stateMachine.AddTransition("patrol", new BoolTransition(enemySeen, true), "chase");
        stateMachine.AddTransition("chase", new BoolTransition(enemySeen, false), "idle");

        stateMachine.SetState(stateMachine.StateFromName("idle"));
    }

    void Update()
    {
        // update parameters
        var gameObjects = perception.GetGameObjects();
        enemySeen.value = (gameObjects.Length != 0);
        enemy = (gameObjects.Length != 0) ? gameObjects[0] : null;
        timer.value -= Time.deltaTime;

        stateMachine.Update();

        animator.SetFloat("speed", movement.velocity.magnitude);
    }

	private void OnGUI()
	{
        GUI.Label(new Rect(10, 10, 300, 20), stateMachine.GetStateName());
	}
}
