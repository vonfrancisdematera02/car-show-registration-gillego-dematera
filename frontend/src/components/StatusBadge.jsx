function StatusBadge({ status }) {
  const statusClasses = {
    Pending: "bg-warning-subtle text-warning-emphasis",
    Confirmed: "bg-primary-subtle text-primary-emphasis",
    CheckedIn: "bg-success-subtle text-success-emphasis",
    Cancelled: "bg-danger-subtle text-danger-emphasis"
  };

  return (
    <span className={`badge rounded-pill ${statusClasses[status] || "bg-secondary-subtle text-secondary-emphasis"}`}>
      {status === "CheckedIn" ? "Checked In" : status}
    </span>
  );
}

export default StatusBadge;
