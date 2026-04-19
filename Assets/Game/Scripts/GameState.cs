using DG.Tweening;
using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public EnemyAIController enemy;
    public Map map;

    public int pointsPerTurn;

    public float timeForEndRound;
    private float currentTimeForEndRoud;
    private bool startSpinning = false;
    public Image imageToFill;
    public Image imageToRotate;
    public TextMeshProUGUI pointsText;

    [HideInInspector]
    public int roundIdx = 0;

    [HideInInspector]
    public int currentPlayerTurnPoints;
    [HideInInspector]
    public int currentEnemyTurnPoints;

    private void Start()
    {
        currentPlayerTurnPoints = pointsPerTurn;
        currentEnemyTurnPoints = pointsPerTurn;

        currentTimeForEndRoud = 0.0f;
        pointsText.text = currentPlayerTurnPoints.ToString();
    }

    private void Update()
    {
        // round timer block
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startSpinning = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (startSpinning)
            {
                currentTimeForEndRoud += Time.deltaTime;
                imageToFill.fillAmount = currentTimeForEndRoud / timeForEndRound;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            startSpinning = false;
            currentTimeForEndRoud = 0.0f;
            imageToFill.fillAmount = 0.0f;
        }

        if (currentTimeForEndRoud >= timeForEndRound)
        {
            UpdateRound();
            imageToFill.fillAmount = 0.0f;
            currentTimeForEndRoud = 0.0f;
            startSpinning = false;
            StartCoroutine(RotateImage(imageToRotate.rectTransform, 1.0f, 180.0f));
        }
    }

    public void DicreasePlayerPoints(int point)
    {
        currentPlayerTurnPoints = Mathf.Clamp(currentPlayerTurnPoints - point, 0, pointsPerTurn);
        pointsText.text = currentPlayerTurnPoints.ToString();
    }

    public void DicreaseEnemyPoints(int point)
    {
        currentEnemyTurnPoints = Mathf.Clamp(currentEnemyTurnPoints - point, 0, pointsPerTurn);
    }

    public void UpdateRound()
    {
        Debug.Log("Round updated");
        currentPlayerTurnPoints = pointsPerTurn;
        pointsText.text = currentPlayerTurnPoints.ToString();

        currentEnemyTurnPoints = pointsPerTurn;
        roundIdx++;
        enemy.DoRandomMove();

        map.ResetCells(new ECellSprite[0]);

    }

    IEnumerator RotateImage(RectTransform target, float duration, float angle)
    {
        float elapsed = 0.0f;
        target.eulerAngles = Vector3.zero;
        float startZ = target.eulerAngles.z;
        float endZ = startZ + angle;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            float z = Mathf.Lerp(startZ, endZ, t);

            target.rotation = Quaternion.Euler(0, 0, z);

            yield return null;
        }

        target.rotation = Quaternion.Euler(0f, 0f, endZ);
    }

}
