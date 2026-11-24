import { useCallback, useEffect, useState } from "react";
import { getRegistrations, getSummary } from "../api/registrationsApi";
import useDebounce from "../hooks/useDebounce";

import KpiCards from "../components/KpiCards";
import RegistrationFilters from "../components/RegistrationFilters";
import RegistrationTable from "../components/RegistrationTable";
import Pagination from "../components/Pagination";

function RegistrationsPage() {
  const [summary, setSummary] = useState(null);
  const [registrations, setRegistrations] = useState([]);

  const [filters, setFilters] = useState({
    participantName: "",
    type: "",
    status: ""
  });

  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalCount, setTotalCount] = useState(0);
  const [totalPages, setTotalPages] = useState(0);

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  const debouncedSearch = useDebounce(filters.participantName, 400);

  const loadSummary = useCallback(async () => {
    try {
      const data = await getSummary();
      setSummary(data);
    } catch {
      setError("Unable to load registration summary.");
    }
  }, []);

  useEffect(() => {
    loadSummary();
  }, [loadSummary]);

  const loadRegistrations = useCallback(async () => {
    setLoading(true);
    setError("");

    try {
      const data = await getRegistrations({
        participantName: debouncedSearch,
        type: filters.type,
        status: filters.status,
        page,
        pageSize
      });

      setRegistrations(data.items);
      setTotalCount(data.totalCount);
      setTotalPages(data.totalPages);
      setPage(data.page);
    } catch {
      setError("Cannot reach the registration data. Please try again.");
    } finally {
      setLoading(false);
    }
  }, [debouncedSearch, filters.type, filters.status, page, pageSize]);

  useEffect(() => {
    loadRegistrations();
  }, [loadRegistrations]);

  const handleFilterChange = (name, value) => {
    setFilters((current) => ({
      ...current,
      [name]: value
    }));
    setPage(1);
  };

  const clearFilters = () => {
    setFilters({
      participantName: "",
      type: "",
      status: ""
    });
    setPage(1);
  };

  const handlePageSizeChange = (value) => {
    setPageSize(value);
    setPage(1);
  };

  const hasActiveFilters =
    filters.participantName || filters.type || filters.status;

  return (
    <main className="container-fluid dashboard-container py-4">
      <header className="dashboard-header mb-4">
        <div>
          <p className="text-uppercase text-muted small fw-semibold mb-1">
            AutoVista Events
          </p>
          <h1 className="h3 fw-bold mb-1">Car Show 2027</h1>
          <p className="text-muted mb-0">
            Registration Management Dashboard
          </p>
        </div>
        <div className="event-date">
          <span className="small text-muted">EVENT LOCATION</span>
          <strong>Sorsogon City Coliseum</strong>
        </div>
      </header>

      <section className="mb-4">
        <div className="section-heading mb-3">
          <h2 className="h5 fw-bold mb-0">Registration Overview</h2>
          <span className="text-muted small">Live summary</span>
        </div>

        {summary ? (
          <KpiCards summary={summary} />
        ) : (
          <p className="text-muted">Loading summary...</p>
        )}
      </section>

      <section className="card dashboard-panel mb-4">
        <div className="card-body">
          <div className="section-heading mb-3">
            <h2 className="h5 fw-bold mb-0">Registrations</h2>
            <span className="text-muted small">
              Manage and monitor participants
            </span>
          </div>

          <RegistrationFilters
            filters={filters}
            onFilterChange={handleFilterChange}
            onClear={clearFilters}
          />
        </div>
      </section>

      <section className="card dashboard-panel">
        <div className="card-body">
          {error ? (
            <div className="alert alert-danger d-flex flex-column flex-sm-row justify-content-between align-items-sm-center gap-2">
              <span>{error}</span>
              <button
                className="btn btn-outline-danger btn-sm"
                onClick={() => {
                  loadRegistrations();
                  loadSummary();
                }}
              >
                Try Again
              </button>
            </div>
          ) : (
            <>
              <RegistrationTable
                registrations={registrations}
                loading={loading}
              />

              {!loading && registrations.length === 0 && hasActiveFilters && (
                <div className="text-center pb-3">
                  <button
                    className="btn btn-outline-primary btn-sm"
                    onClick={clearFilters}
                  >
                    Clear Filters
                  </button>
                </div>
              )}

              <Pagination
                page={page}
                totalPages={totalPages}
                totalCount={totalCount}
                pageSize={pageSize}
                onPageChange={setPage}
                onPageSizeChange={handlePageSizeChange}
                loading={loading}
              />
            </>
          )}
        </div>
      </section>

      <footer className="text-center text-muted small mt-4">
        AutoVista Events © 2027 | Car Show Registration System
      </footer>
    </main>
  );
}

export default RegistrationsPage;

