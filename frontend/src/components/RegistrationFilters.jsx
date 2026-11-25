function RegistrationFilters({ filters, onFilterChange, onClear }) {
  return (
    <div className="row g-3 align-items-end">
      <div className="col-12 col-lg-5">
        <label className="form-label fw-semibold">
          Search Participant
        </label>
        <input
          type="search"
          className="form-control"
          placeholder="Search by participant name..."
          value={filters.participantName}
          onChange={(event) =>
            onFilterChange("participantName", event.target.value)
          }
        />
      </div>

      <div className="col-12 col-sm-6 col-lg-3">
        <label className="form-label fw-semibold">
          Registration Type
        </label>
        <select
          className="form-select"
          value={filters.type}
          onChange={(event) => onFilterChange("type", event.target.value)}
        >
          <option value="">All Types</option>
          <option value="Exhibitor">Exhibitor</option>
          <option value="Competitor">Competitor</option>
          <option value="VIP">VIP</option>
        </select>
      </div>

      <div className="col-12 col-sm-6 col-lg-2">
        <label className="form-label fw-semibold">Status</label>
        <select
          className="form-select"
          value={filters.status}
          onChange={(event) => onFilterChange("status", event.target.value)}
        >
          <option value="">All Statuses</option>
          <option value="Pending">Pending</option>
          <option value="Confirmed">Confirmed</option>
          <option value="CheckedIn">Checked In</option>
          <option value="Cancelled">Cancelled</option>
        </select>
      </div>

      <div className="col-12 col-lg-2">
        <button
          type="button"
          className="btn btn-outline-secondary w-100"
          onClick={onClear}
        >
          Clear Filters
        </button>
      </div>
    </div>
  );
}

export default RegistrationFilters;
