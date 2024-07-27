using System;
using System.Threading.Tasks;
using CodeBase.Sources.Modules.Wallet.Balances;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.Sources.Modules.Wallet.View
{
    public class CoinBalanceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _balanceText;

        [SerializeField] private float _punchScaleCoefficient;
        [SerializeField] private float _punchTime;
        [SerializeField] private float _timeToAddCoin;
        [SerializeField] private int _punchStep;

        private int _targetBalance;
        private bool _isBalanceUpdating;
        
        private IWallet _walletInstance;
        private BalanceBase _coinBalance;
        
        [Inject]
        public void Construct(IWallet wallet)
        {
            _walletInstance = wallet;
            _coinBalance = _walletInstance.GetCurrencyBalance(CurrencyType.Coin);
        }

        private void OnEnable()
        {
            if (_walletInstance != null)
                _walletInstance.BalanceUpdated += UpdateView;
        }

        private void Start()
        {
            SetDisplayedBalance(_coinBalance.Balance);
        }

        private void OnDisable()
        {
            if (_walletInstance != null)
                _walletInstance.BalanceUpdated -= UpdateView;
        }

        private void UpdateView()
        {
            if (_isBalanceUpdating == false)
                SetNewBalance();
        }

        private async void SetNewBalance()
        {
            _isBalanceUpdating = true;

            int updatedCoins = 0;
            for (int currentBalance = DisplayedBalance; currentBalance <= TargetBalance; currentBalance++)
            {
                await UniTask.WaitForSeconds(_timeToAddCoin);
                SetDisplayedBalance(currentBalance);
                updatedCoins++;
                
                if (updatedCoins >= _punchStep)
                {
                    updatedCoins = 0;
                    await DoTextPunch();
                }
            }
            
            _isBalanceUpdating = false;
        }

        private void SetDisplayedBalance(int toSet) => 
            _balanceText.text = toSet.ToString();

        private async Task DoTextPunch()
        {
            const float punchSteps = 3;
            float originalFont = _balanceText.fontSize;

            float stepTime = _punchTime / punchSteps;
            
            float pastTime = 0;
            while (pastTime <=stepTime)
            {
                await UniTask.NextFrame();
                pastTime += Time.deltaTime;

                _balanceText.fontSize = Mathf.Lerp(originalFont, originalFont * _punchScaleCoefficient, pastTime / stepTime);
            }

            await UniTask.WaitForSeconds(stepTime);

            pastTime = 0;
            while (pastTime <=stepTime)
            {
                await UniTask.NextFrame();
                pastTime += Time.deltaTime;

                _balanceText.fontSize = Mathf.Lerp(originalFont * _punchScaleCoefficient, originalFont, pastTime / stepTime);
            }

            _balanceText.fontSize = originalFont;
        }

        private int TargetBalance => _coinBalance.Balance;
        private int DisplayedBalance => int.Parse(_balanceText.text);
    }
}