import StatusBadge from "./StatusBadge";
import PaymentBadge from "./PaymentBadge";

const peso = (amount) =>
  new Intl.NumberFormat("en-PH", {
    style: "currency",
    currency: "PHP"
  }).format(amount ?? 0);

const formatDate = (date) =>
  new Intl.DateTimeFormat("en-PH", {
    month: "short",
    day: "2-digit",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
    hour12: true
  }).format(new Date(date));

function RegistrationTable({ registrations, loading }) {
  if (loading) {
    return <div className="text-center py-5">Loading registrations...</div>;
  }

  if (!registrations?.length) {
    return (
      <div className="text-center py-5 text-muted">
        No registrations match your filters.
      </div>
    );
  }

  return (
    <div className="table-responsive">
      <table className="table align-middle registration-table">
        <thead>
          <tr>
            <th>Registration No.</th>
            <th>Date</th>
            <th>Participant</th>
            <th>Car / Plate No.</th>
            <th>Type</th>
            <th>Status</th>
            <th>Fee / Paid</th>
            <th>Payment</th>
          </tr>
        </thead>
        <tbody>
          {registrations.map((registration) => (
            <tr key={registration.id}>
              <td className="fw-semibold">
                {registration.registrationNumber}
              </td>
              <td className="text-nowrap">
                {formatDate(registration.registrationDate)}
              </td>
              <td>
                <div className="fw-semibold">
                  {registration.participantName}
                </div>
                <small className="text-muted d-block">
                  {registration.contactNumber}
                </small>
                <small className="text-muted">
                  {registration.email}
                </small>
              </td>
              <td>
                <div>{registration.carMakeModel}</div>
                <small className="text-muted">
                  {registration.plateNumber}
                </small>
              </td>
              <td>{registration.registrationType}</td>
              <td><StatusBadge status={registration.status} /></td>
              <td className="text-nowrap">
                <div>{peso(registration.registrationFee)}</div>
                <small className="text-muted">
                  Paid: {peso(registration.amountPaid)}
                </small>
              </td>
              <td>
                <PaymentBadge paymentStatus={registration.paymentStatus} />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default RegistrationTable;
