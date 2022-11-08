using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace PrismBaseBallGame.ViewModels
{
    public class GameViewModel : BindableBase
    {
        string solNum;


        public string getNum()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            List<int> numList = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                numList.Add(i);
            }
            string str = "";
            for (int i = 0; i < 3; i++)
            {
                int index = rand.Next(0, 10 - i);
                str += numList[index];
                numList.RemoveAt(index);
            }
            return str;
        }

        public GameViewModel()
        {
            solNum = getNum();
            HistoryText = "[기록]";
            HistoryVisible = false;
        }

        private int _strikeCount;
        public int StrikeCount
        {
            get { return _strikeCount; }
            set { SetProperty(ref _strikeCount, value); }
        }

        private int _ballCount;
        public int BallCount
        {
            get { return _ballCount; }
            set { SetProperty(ref _ballCount, value); }
        }

        private int _outCount;
        public int OutCount
        {
            get { return _outCount; }
            set { SetProperty(ref _outCount, value); }
        }

        private int _tryCount;
        public int TryCount
        {
            get { return _tryCount; }
            set { SetProperty(ref _tryCount, value); }
        }

        private string _inputBox;
        public string InputBox
        {
            get { return _inputBox; }
            set 
            {
                if (value == null || !Regex.IsMatch(value, "[^0-9]+"))
                {
                    SetProperty(ref _inputBox, value);
                    SubmissionClick.RaiseCanExecuteChanged();
                }
            }
        }

        private DelegateCommand _submissionClick;
        public DelegateCommand SubmissionClick =>
            _submissionClick ?? (_submissionClick = new DelegateCommand(ExecuteCommandName,CanSubmission));

        private bool CanSubmission()
        {
            var isvallidExpression = InputBox != null ? !Regex.IsMatch(InputBox, "[^0-9]+") && (InputBox.Length == 3) : false;
            return isvallidExpression;
        }

        void ExecuteCommandName()
        {
            TryCount++;
            int s = 0;
            int b = 0;
            int o = 0;
            string ans = InputBox;
            for (int i = 0; i < 3; i++)
            {
                if (solNum[i].Equals(ans[i])) s++;
                else if (solNum.Contains(ans[i])) b++;
                else o++;
            }
            StrikeCount = s;
            BallCount = b;
            OutCount = o;
            HistoryText += "\n시도횟수: " + TryCount + "  입력숫자: " + ans + "  S" + StrikeCount + "B" + BallCount + "O" + OutCount;
            if (StrikeCount == 3)
            {
                MessageBox.Show(TryCount + "회");
                Clear();
            }
        }
        private DelegateCommand _inputClear;
        public DelegateCommand InputClear =>
            _inputClear ?? (_inputClear = new DelegateCommand(ExecuteInputClear));

        void ExecuteInputClear()
        {
            InputBox = null;
        }

        private DelegateCommand _historyCommand;
        public DelegateCommand HistoryCommand =>
            _historyCommand ?? (_historyCommand = new DelegateCommand(ExecuteHistoryCommand));

        void ExecuteHistoryCommand()
        {
            HistoryVisible = HistoryVisible ? false : true;  
        }

        private bool _historyVisible;
        public bool HistoryVisible
        {
            get { return _historyVisible; }
            set 
            { 
                SetProperty(ref _historyVisible, value); 
            }
        }

        private string _historyText;
        public string HistoryText
        {
            get { return _historyText; }
            set { SetProperty(ref _historyText, value); }
        }

        public void Clear()
        {
            solNum = getNum();
            HistoryText = "[기록]";
            HistoryVisible = false;
            StrikeCount = 0;
            BallCount = 0;
            OutCount = 0;
            TryCount = 0;
            InputBox = null;
        }
    }
}
