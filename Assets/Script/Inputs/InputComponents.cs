namespace Excappalitas.Inputs {
    /// <sumary> Component of an Input </summary>
    public class InputComponents {

        public string Name;
        public bool HasInput;
        public float Value;
 
        internal InputComponents(string name, bool hasInput = false , float value = 0.0f) {
            Name = name;
            HasInput = hasInput;
            Value = value;
        }
    }
}