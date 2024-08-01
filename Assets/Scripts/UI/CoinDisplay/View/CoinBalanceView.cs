using System;
using APIs.CoroutineRunner;
using MVPBase;
using TMPro;
using UnityEngine;
using Utils.Extensions;

namespace UI.CoinDisplay.View
{
    public class CoinBalanceView : ViewBase, ICoroutineRunner
    {
        [SerializeField] private TMP_Text _balanceText;
        public event Action Destroying;
        public float OriginalFontSize { get; private set; }
        public int DisplayedBalance => _balanceText.text.AsInt();

        public void SetDisplayedBalance(int toSet) => 
            _balanceText.text = toSet.ToString();

        public void SetFontSize(float toSet) => 
            _balanceText.fontSize = toSet;

        private void Awake() => 
            OriginalFontSize = _balanceText.fontSize;

        private void OnDestroy() => 
            Destroying?.Invoke();
    }
}