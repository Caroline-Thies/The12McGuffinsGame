using UnityEngine.Events;

public class UserPrompt {

    public class SubmitEvent : UnityEvent<string> {}

    public SubmitEvent onSubmit = new SubmitEvent();
}