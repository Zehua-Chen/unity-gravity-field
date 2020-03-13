using UnityEngine;
using UnityEngine.SceneManagement;
using GravityField.UI;

namespace GravityField.SceneControllers
{
    public class WelcomeSceneController : MonoBehaviour
    {
        [SerializeField]
        string _mainScene = "Main";

        [SerializeField]
        ProgressBar _progressBar = null;

        float _progress = 0.0f;

        AsyncOperation _asyncOperation = null;

        private void Start()
        {
            _progressBar.Progress = _progress;
        }

        private void Update()
        {
            if (_asyncOperation != null)
            {
                _progress = _asyncOperation.progress;
            }

            _progressBar.Progress = _progress;
        }

        public void OnStart()
        {
            _asyncOperation = SceneManager.LoadSceneAsync(_mainScene);
        }
    }
}
