import KpiCard from "./KpiCard";

const peso = (amount) =>
  new Intl.NumberFormat("en-PH", {
    style: "currency",
    currency: "PHP"
  }).format(amount ?? 0);

function KpiCards({ summary }) {
  const cards = [
    {
      title: "Total Registrations",
      value: summary?.totalRegistrations ?? 0,
      icon: "▤"
    },
    {
      title: "Total Payments",
      value: peso(summary?.totalPayments),
      icon: "₱"
    },
    {
      title: "Uncollected Payments",
      value: peso(summary?.uncollectedPayments),
      icon: "◷"
    },
    {
      title: "Confirmed",
      value: summary?.confirmed ?? 0,
      icon: "✓"
    },
    {
      title: "Checked In",
      value: summary?.checkedIn ?? 0,
      icon: "↪"
    }
  ];

  return (
    <div className="row row-cols-1 row-cols-sm-2 row-cols-lg-5 g-3">
      {cards.map((card) => (
        <KpiCard
          key={card.title}
          title={card.title}
          value={card.value}
          icon={card.icon}
        />
      ))}
    </div>
  );
}

export default KpiCards;
