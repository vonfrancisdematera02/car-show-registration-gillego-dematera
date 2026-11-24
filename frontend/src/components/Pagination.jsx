function Pagination({
  page,
  totalPages,
  totalCount,
  pageSize,
  onPageChange,
  onPageSizeChange,
  loading
}) {
  const start = totalCount === 0 ? 0 : (page - 1) * pageSize + 1;
  const end = Math.min(page * pageSize, totalCount);

  const pages = Array.from(
    { length: totalPages },
    (_, index) => index + 1
  );

  return (
    <div className="d-flex flex-column flex-lg-row justify-content-between align-items-center gap-3 mt-3">
      <div className="d-flex align-items-center gap-2">
        <small className="text-muted text-nowrap">
          Showing {start}–{end} of {totalCount}
        </small>

        <select
          className="form-select form-select-sm"
          style={{ width: "auto" }}
          value={pageSize}
          disabled={loading}
          onChange={(event) => onPageSizeChange(Number(event.target.value))}
          aria-label="Rows per page"
        >
          <option value={5}>5 per page</option>
          <option value={10}>10 per page</option>
          <option value={20}>20 per page</option>
        </select>
      </div>

      <nav aria-label="Registration pages">
        <ul className="pagination pagination-sm mb-0">
          <li className={`page-item ${page <= 1 || loading ? "disabled" : ""}`}>
            <button
              className="page-link"
              disabled={page <= 1 || loading}
              onClick={() => onPageChange(page - 1)}
            >
              Previous
            </button>
          </li>

          {pages.map((pageNumber) => (
            <li
              key={pageNumber}
              className={`page-item ${pageNumber === page ? "active" : ""}`}
            >
              <button
                className="page-link"
                disabled={loading}
                onClick={() => onPageChange(pageNumber)}
              >
                {pageNumber}
              </button>
            </li>
          ))}

          <li className={`page-item ${page >= totalPages || loading ? "disabled" : ""}`}>
            <button
              className="page-link"
              disabled={page >= totalPages || loading}
              onClick={() => onPageChange(page + 1)}
            >
              Next
            </button>
          </li>
        </ul>
      </nav>
    </div>
  );
}

export default Pagination;
