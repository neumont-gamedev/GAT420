using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UtilityAgent : Agent
{
    [SerializeField] Perception perception;
    [SerializeField] MeterUI meter;

	const float MIN_SCORE = 0.1f;

	Need[] needs;
    UtilityObject activeUtilityObject = null;
    public bool isUsingUtilityObject { get { return activeUtilityObject != null; } }

    public float happiness
	{
        get
		{
            float totalMotive = 0;
			foreach (var need in needs)
			{
				totalMotive += need.motive;
			}

            return 1 - (totalMotive / needs.Length);
		}
	}


    void Start()
    {
        needs = GetComponentsInChildren<Need>();

        meter.text.text = "";
    }
    
    void Update()
    {
        animator.SetFloat("Speed", movement.velocity.magnitude);

        if (activeUtilityObject == null)
		{
            var gameObjects = perception.GetGameObjects();
            List<UtilityObject> utilityObjects = new List<UtilityObject>();
            foreach (var go in gameObjects)
			{
                if (go.TryGetComponent<UtilityObject>(out UtilityObject utilityObject))
				{
                    utilityObject.visible = true;
                    utilityObject.score = GetUtilityObjectScore(utilityObject);
                    if (utilityObject.score > MIN_SCORE) utilityObjects.Add(utilityObject);
				}
			}

            // set active utility object to first utility object
            activeUtilityObject = (utilityObjects.Count == 0) ? null : utilityObjects[0];
            if (activeUtilityObject != null)
			{
                StartCoroutine(ExecuteUtilityObject(activeUtilityObject));
			}
		}
        else
		{
            activeUtilityObject.visible = true;
            activeUtilityObject.score = GetUtilityObjectScore(activeUtilityObject);
        }
    }

	private void LateUpdate()
	{
        meter.slider.value = happiness;
        meter.worldPosition = transform.position + Vector3.up * 4;
    }

    IEnumerator ExecuteUtilityObject(UtilityObject utilityObject)
	{
		// go to location
		movement.MoveTowards(utilityObject.location.position);

        yield return new WaitUntil(() => Vector3.Distance(transform.position, utilityObject.location.position) < 0.5f);

        // transform to action location
        ((NavMeshMovement)movement).navMeshAgent.enabled = false;
        if (utilityObject.actionLocation != null)
        {
            float timer = 0;
            // set the start transform from the current transorm
            Transform startTransform = transform;
            while (timer < 1)
            {
                timer += Time.deltaTime;
                timer = Mathf.Min(timer, 1);

                // interpolate from start transform to action location
                Utilities.Lerp(startTransform, utilityObject.actionLocation, transform, timer);
                yield return null;
            }
        }

        // play animation
        animator.SetBool(utilityObject.actionAnimation, true);

        // start effect
        if (utilityObject.effect != null) utilityObject.effect.SetActive(true);

        // wait duration
        yield return new WaitForSeconds(utilityObject.duration);

        // stop animation
        animator.SetBool(utilityObject.actionAnimation, false);

        // stop effect
        if (utilityObject.effect != null) utilityObject.effect.SetActive(false);

        // transform to location
        ((NavMeshMovement)movement).navMeshAgent.enabled = false;
        if (utilityObject.actionLocation != null)
        {
            float timer = 0;
            // set the start transform from the current transorm
            Transform startTransform = transform;
            while (timer < 1)
            {
                timer += Time.deltaTime;
                timer = Mathf.Min(timer, 1);

                // interpolate from start transform to location
                Utilities.Lerp(startTransform, utilityObject.location, transform, timer);
                yield return null;
            }
        }
        ((NavMeshMovement)movement).navMeshAgent.enabled = true;


        // apply utility object effectors
        ApplyUtilityObject(utilityObject);

        activeUtilityObject = null;

        yield return null;
	}

    void ApplyUtilityObject(UtilityObject utilityObject)
    {
        foreach (var effector in utilityObject.effectors)
        {
            Need need = GetNeedByType(effector.type);
            if (need != null)
			{
                need.input += effector.change;
                need.input = Mathf.Clamp(need.input, -1, 1);
			}
        }
    }


	float GetUtilityObjectScore(UtilityObject utilityObject)
	{
        float score = 0;

        foreach (var effector in utilityObject.effectors)
		{
            Need need = GetNeedByType(effector.type);
            if (need != null)
			{
                float futureNeed = need.getMotive(need.input + effector.change);
                score += need.motive - futureNeed;
			}
		}

        return score;
	}

    Need GetNeedByType(Need.Type type)
	{
        return needs.First(need => need.type == type);
	}
}
