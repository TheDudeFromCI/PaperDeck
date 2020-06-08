using UnityEngine;
using SimpleFileBrowser;
using System.Collections;
using UnityEngine.UI;

namespace PaperDeck.Menu.CardBuilder
{
    public class ImageUploader : MonoBehaviour
    {
        [Tooltip("The image slot on the card to upload to.")]
        [SerializeField] protected Image m_ImageTarget;

        private Sprite m_LastSprite;
        private Texture2D m_Texture;

        /// <summary>
        /// Called when the scene is loaded to create the buffer texture.
        /// </summary>
        protected void Awake()
        {
            m_Texture = new Texture2D(2, 2);
        }

        /// <summary>
        /// Called when the scene is unloaded to clean up resources.
        /// </summary>
        protected void OnDestroy()
        {
            Destroy(m_Texture);
            Destroy(m_LastSprite);
        }

        /// <summary>
        /// Opens a file browser for the user to select an image to apply to the
        /// card.
        /// </summary>
        public void UploadImage()
        {
            StartCoroutine(DoImageUpload());
        }

        /// <summary>
        /// Loads the raw file bytes into a sprite and applies it to the card.
        /// </summary>
        /// <param name="bytes">The file byte array.</param>
        private void ApplyImageToCard(byte[] bytes)
        {
            if (m_LastSprite != null)
                Destroy(m_LastSprite);

            m_Texture.LoadImage(bytes, true);

            var rect = new Rect(0, 0, m_Texture.width, m_Texture.height);
            var pivot = new Vector2(m_Texture.width, m_Texture.height) / 2;
            m_LastSprite = Sprite.Create(m_Texture, rect, pivot);

            m_ImageTarget.sprite = m_LastSprite;
        }

        /// <summary>
        /// Resets the image position back to identity.
        /// </summary>
        private void ResetImagePosition()
        {
            m_ImageTarget.SetNativeSize();
            m_ImageTarget.transform.localPosition = Vector3.zero;
            m_ImageTarget.transform.localRotation = Quaternion.identity;
            m_ImageTarget.transform.localScale = Vector3.one;
        }

        /// <summary>
        /// Opens the file browser to upload an image, and apply it to the card
        /// if an image is selected.
        /// </summary>
        /// <returns>The coroutine process.</returns>
        private IEnumerator DoImageUpload()
        {
            if (FileBrowser.IsOpen)
                yield break;

            FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
            // FileBrowser.SetDefaultFilter(".png");

            yield return FileBrowser.WaitForLoadDialog(false, false, null, "Select Image", "Load");

            if (FileBrowser.Success)
            {
                var result = FileBrowser.Result[0];
                Debug.Log($"Uploading image {result}");

                var bytes = FileBrowserHelpers.ReadBytesFromFile(result);
                ApplyImageToCard(bytes);
                ResetImagePosition();
            }
        }
    }
}