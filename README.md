# Multi-Database Transaction Laboratory (.NET Framework)

This is a simple .NET Framework console application built to demonstrate how **MSDTC (Microsoft Distributed Transaction Coordinator)** automatically manages distributed transactions across multiple distinct SQL Server databases using `TransactionScope`.

---

## Architecture Overview

When you perform database operations across different servers or databases within a `TransactionScope`, .NET automatically promotes the local transaction to a distributed transaction managed by the Windows MSDTC service using a **Two-Phase Commit (2PC)** protocol.

### How it Works:
1. **Phase 1 (Prepare):** MSDTC asks both databases if they are ready to commit the changes.
2. **Phase 2 (Commit/Rollback):** If both reply "Ready", MSDTC commits the data permanently. If any database fails or a network issue occurs, MSDTC rolls back changes in **all** databases to prevent data inconsistency.

---

![Flow](https://raw.githubusercontent.com/bbarreto08/MSDTC.Sample/refs/heads/main/docs/msdtc.png)

---

## Prerequisites

Before running the application, ensure that:
1. You are running on **Windows**.
2. The **Distributed Transaction Coordinator** service is running (`services.msc`).
3. Network DTC Access, Inbound, and Outbound connections are enabled in `dcomcnfg` (Component Services).

---

