function PaymentBadge({ paymentStatus }) {
  const paymentClasses = {
    Unpaid: "bg-danger-subtle text-danger-emphasis",
    Partial: "bg-warning-subtle text-warning-emphasis",
    Paid: "bg-success-subtle text-success-emphasis"
  };

  return (
    <span className={`badge rounded-pill ${paymentClasses[paymentStatus] || "bg-secondary-subtle text-secondary-emphasis"}`}>
      {paymentStatus || "Unknown"}
    </span>
  );
}

export default PaymentBadge;
