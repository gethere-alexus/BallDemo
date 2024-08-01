using UnityEngine;

namespace Ball.View.Modules.Material
{
    public class BallMaterialView : MonoBehaviour
    {
        private UnityEngine.Material _ballMaterial;
        
        public Color OriginalMaterialColor { get; private set; }

        public void SetMaterialColor(Color toSet) => 
            _ballMaterial.color = toSet;
        
        private void Awake()
        {
            _ballMaterial = GetComponent<MeshRenderer>().material;
            OriginalMaterialColor = _ballMaterial.color;
        }
    }
}