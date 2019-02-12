using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GenericAnimationFormsSample.Behaviors
{
    public class EntryShakeBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty ShakeProperty =
         BindableProperty.Create(nameof(Shake), typeof(ICommand), typeof(ViewTappedButtonBehavior), null, defaultBindingMode: BindingMode.TwoWay);

        public ICommand Shake
        {
            get { return (ICommand)GetValue(ShakeProperty); }
            set { SetValue(ShakeProperty, value); }
        }
        public Entry AssociatedObject { get; private set; }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;


            base.OnDetachingFrom(bindable);
        }

        bool _isAnimating = false;

        void ShakeIt()
        {
            if (_isAnimating)
                return;

            _isAnimating = true;

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    await AssociatedObject.TranslateTo(-15, 0, 50);
                    await AssociatedObject.TranslateTo(15, 0, 50);
                    await AssociatedObject.TranslateTo(-10, 0, 50);
                    await AssociatedObject.TranslateTo(10, 0, 50);
                    await AssociatedObject.TranslateTo(-5, 0, 50);
                    await AssociatedObject.TranslateTo(5, 0, 50);
                    AssociatedObject.TranslationX = 0;

                }
                finally
                {
                    _isAnimating = false;
                }
            });
        }

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;

            if(BindingContext!= null)
            {
                Shake = new Command(() =>
                {
                    ShakeIt();
                });
            }
        }
    }
}
