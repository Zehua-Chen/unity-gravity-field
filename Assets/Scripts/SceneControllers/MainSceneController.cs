using UnityEngine;
using UnityEngine.SceneManagement;

namespace GravityField.SceneControllers
{
    public class MainSceneController : MonoBehaviour
    {
        [SerializeField]
        Camera _camera = null;

        [SerializeField]
        bool _introduction = true;

        [Header("Planet Spawn Settings")]
        [SerializeField]
        GameObject _marsPrefab = null;

        [SerializeField]
        float _marsScale = 0.3f;

        [Header("UI")]
        [SerializeField]
        GameObject _introductionPanel = null;

        private void Update()
        {
            if (!_introduction && Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            }

            if (!_introduction && Input.GetMouseButtonDown(0))
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

        public void OnAcknoledge()
        {
            GameObject.Destroy(_introductionPanel);
            _introduction = false;
        }
    }
}
