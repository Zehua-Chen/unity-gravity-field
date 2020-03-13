using System.Collections;
using UnityEngine;

namespace GravityField.SceneControllers
{
    public class MainSceneController : MonoBehaviour
    {
        [SerializeField]
        Camera _camera = null;

        [Header("Planet Spawn Settings")]
        [SerializeField]
        GameObject _marsPrefab = null;

        [SerializeField]
        float _marsScale = 0.3f;

        [Header("UI")]
        [SerializeField]
        GameObject _promptText = null;

        [SerializeField]
        float _visibleTime = 2.0f;

        [SerializeField]
        float _disappearDuration = 1.0f;

        private void Start()
        {
            StartCoroutine(DestroyPrompt());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateMars(_camera.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        private void CreateMars(Vector3 position)
        {
            GameObject mars = GameObject.Instantiate(_marsPrefab);
            Transform transform = mars.GetComponent<Transform>();

            position.z = 0.0f;

            transform.position = position;
            transform.localScale = new Vector3(_marsScale, _marsScale, 0.0f);
        }

        private IEnumerator DestroyPrompt()
        {
            yield return new WaitForSecondsRealtime(_visibleTime);

            AnimationCurve curve = AnimationCurve.EaseInOut(0.0f, 1.0f, _disappearDuration, 0.0f);
            CanvasRenderer renderer = _promptText.GetComponent<CanvasRenderer>();

            for (float t = 0.0f; t < _disappearDuration; t += Time.deltaTime)
            {
                renderer.SetAlpha(curve.Evaluate(t));
                yield return null;
            }

            GameObject.Destroy(_promptText);
        }
    }
}
