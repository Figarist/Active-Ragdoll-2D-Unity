using UnityEngine;
using UnityEngine.UI;

namespace ActiveRagdoll.UI
{
	public class HealthBar : MonoBehaviour
	{

		private Slider _slider;
		public Gradient gradient;
		private Image fill;

		private void Start()
		{
			_slider = GetComponent<Slider>();
			fill = transform.GetChild(0).GetComponent<Image>();
		}

		public void SetMaxHealth(int health)
		{
			_slider.maxValue = health;
			_slider.value = health;
			fill.color = gradient.Evaluate(1f);
		}

		public void SetMinusHealth(int health)
		{
			_slider.value -= health;
			fill.color = gradient.Evaluate(_slider.normalizedValue);
		}

		public float GetHealth() { return _slider.value; }
	}
}
