# Multi-Model Agent Handoffs (`docs/agent-handoffs/README.md`)

In accordance with Section 10.4 (*Intelligent Model Selection and Multi-Model Orchestration*) of the NAWWAR specification, whenever a task transitions between model roles (e.g., from a **Premium reasoning model** to a **Strong coding model** or **Fast model**, or from implementation to independent review), the outgoing agent must create or update a handoff brief in this directory named:

```text
docs/agent-handoffs/HANDOFF_<TASK_NAME>.md
```

---

## Mandatory Handoff Sections

Every `HANDOFF_<TASK_NAME>.md` file must include:
1. **Project name** (`NAWWAR`) & **Current project phase**
2. **Task objective**
3. **Relevant architectural context**
4. **Files inspected**
5. **Files modified**
6. **Implementation completed**
7. **Current technical state**
8. **Outstanding issues**
9. **Constraints and decisions that must not be changed**
10. **Required tests**
11. **Expected deliverables**
12. **Recommended model role**, **Recommended thinking level**, and **Recommended execution mode** (`Planning` vs `Fast`)

The incoming agent must read [`AGENTS.md`](../../AGENTS.md), [`ARCHITECTURE.md`](../../ARCHITECTURE.md), [`PROGRESS.md`](../../PROGRESS.md), and the active handoff document before modifying code.

