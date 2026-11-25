function KpiCard({ title, value, subtitle, icon }) {
  return (
    <div className="col">
      <div className="card h-100 kpi-card">
        <div className="card-body">
          <div className="d-flex justify-content-between align-items-start">
            <div>
              <p className="kpi-title mb-2">{title}</p>
              <h3 className="kpi-value mb-1">{value}</h3>
              {subtitle && (
                <small className="text-muted">{subtitle}</small>
              )}
            </div>
            <div className="kpi-icon">{icon}</div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default KpiCard;
