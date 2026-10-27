public interface IPaymentProcessor
{
  void Pay(decimal amountVnd);
}

public class StripeSdk
{
  public void Charge(long amountCents, string currency)
  {
    Console.WriteLine($"Stripe charged {amountCents} {currency}");
  }
}

public class StripeAdapter : IPaymentProcessor
{
  private readonly StripeSdk _stripe;

  public StripeAdapter(StripeSdk stripe)
  {
    _stripe = stripe;
  }

  public void Pay(decimal amountVnd)
  {
    long cents = (long)(amountVnd * 100);
    _stripe.Charge(cents, "VND");
  }
}

public class CheckoutService
{
  private readonly IPaymentProcessor _processor;

  public CheckoutService(IPaymentProcessor processor)
  {
    _processor = processor;
  }

  public void Checkout(decimal total)
  {
    _processor.Pay(total);
  }
}

IPaymentProcessor processor = new StripeAdapter(new StripeSdk());
CheckoutService checkout = new CheckoutService(processor);
checkout.Checkout(150000m);
