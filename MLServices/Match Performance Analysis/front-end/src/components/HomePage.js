import React from 'react';
import { ArrowRight, Bot, ChartArea, Compass, GitCompareArrows, ListChecks, Sparkles } from 'lucide-react';
import { PAGES } from '../context/AppContext';

const HomePage = ({ onNavigate }) => {
  const cards = [
    {
      title: 'Player Workspace',
      description: 'Search players quickly, open profile dashboards, and inspect decision-ready metrics.',
      target: PAGES.PLAYERS,
      icon: ListChecks,
    },
    {
      title: 'Performance Dashboard',
      description: 'Track season-level and match-specific visuals with model scoring context.',
      target: PAGES.DASHBOARD,
      icon: ChartArea,
    },
    {
      title: 'Comparison Studio',
      description: 'Run side-by-side comparisons to identify strengths, fit, and role suitability.',
      target: PAGES.COMPARISON,
      icon: GitCompareArrows,
    },
    {
      title: 'API Validation',
      description: 'Verify endpoint health, payload shape, and latency for robust integration.',
      target: PAGES.API_TESTER,
      icon: Compass,
    },
  ];

  return (
    <div className="space-y-6">
      <section className="surface overflow-hidden p-8 sm:p-10">
        <div className="grid gap-8 lg:grid-cols-[1.3fr_1fr] lg:items-center">
          <div>
            <div className="mb-4 inline-flex items-center gap-2 rounded-full border border-brand-200 bg-brand-50 px-3 py-1 text-xs font-semibold text-brand-700">
              <Sparkles className="h-3.5 w-3.5" />
              Real-world football analytics workflow
            </div>
            <h2 className="text-3xl font-semibold text-slate-900 sm:text-4xl">
              Production-grade performance intelligence for squads, analysts, and coaches
            </h2>
            <p className="mt-4 max-w-2xl text-sm leading-6 text-slate-600 sm:text-base">
              Unified interface for xG, VAEP, role-based benchmarking, and player-level recommendations.
              Built for fast iteration, reliable API integration, and adaptable AI-enhanced modules.
            </p>
            <div className="mt-6 flex flex-wrap gap-3">
              <button className="btn-primary" onClick={() => onNavigate(PAGES.PLAYERS)}>
                Open Player Workspace
                <ArrowRight className="ml-2 h-4 w-4" />
              </button>
              <button className="btn-secondary" onClick={() => onNavigate(PAGES.COMPARISON)}>
                Open Comparison
              </button>
            </div>
          </div>

          <div className="surface-muted p-6">
            <div className="flex items-start gap-3">
              <div className="rounded-lg bg-slate-900 p-2 text-white">
                <Bot className="h-5 w-5" />
              </div>
              <div>
                <h3 className="text-sm font-semibold text-slate-900">AI-ready content zone</h3>
                <p className="mt-2 text-sm text-slate-600">
                  This area is intentionally structured for future recommendation feeds, player suggestions,
                  and conversational insights connected to model outputs.
                </p>
              </div>
            </div>
          </div>
        </div>
      </section>

      <section className="grid gap-4 sm:grid-cols-2">
        {cards.map((card) => (
          <button
            key={card.title}
            type="button"
            onClick={() => onNavigate(card.target)}
            className="surface group p-6 text-left transition hover:-translate-y-0.5 hover:shadow-xl focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-brand-500"
          >
            <div className="mb-4 inline-flex rounded-lg bg-slate-900 p-2 text-white transition group-hover:bg-brand-600">
              <card.icon className="h-5 w-5" />
            </div>
            <h3 className="text-lg font-semibold text-slate-900">{card.title}</h3>
            <p className="mt-2 text-sm leading-6 text-slate-600">{card.description}</p>
          </button>
        ))}
      </section>

      <section className="surface p-6">
        <h3 className="text-lg font-semibold text-slate-900">Delivery standards in this UI</h3>
        <ul className="mt-3 grid gap-2 text-sm text-slate-600 sm:grid-cols-2">
          <li>Functional components and hooks only</li>
          <li>Tailwind-first responsive architecture</li>
          <li>Accessible focus states and contrast-aware surfaces</li>
          <li>Stable loading, error, and empty-state handling</li>
        </ul>
      </section>
    </div>
  );
};

export default HomePage;
