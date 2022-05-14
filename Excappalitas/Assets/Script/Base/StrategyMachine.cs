
namespace Excappalitas {
    public class StrategyMachine {

        public IStrategy Strategy {
            get {
                return _currentStrategy;
            }
            set {
                _currentStrategy = value;
            }
        }
        private IStrategy _currentStrategy;

        public StrategyMachine () {}
        public StrategyMachine (IStrategy strategy) {
            _currentStrategy = strategy;
        }

        public void ExecuteStrategy () {
            _currentStrategy.Execute ();
        }
    }
}