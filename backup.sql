--
-- PostgreSQL database dump
--

-- Dumped from database version 15.12 (Debian 15.12-1.pgdg120+1)
-- Dumped by pg_dump version 15.12 (Debian 15.12-1.pgdg120+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Todos; Type: TABLE; Schema: public; Owner: myuser
--

CREATE TABLE public."Todos" (
    "Id" uuid NOT NULL,
    "Title" text NOT NULL,
    "IsCompleted" boolean NOT NULL,
    "Description" text NOT NULL
);


ALTER TABLE public."Todos" OWNER TO myuser;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: myuser
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO myuser;

--
-- Data for Name: Todos; Type: TABLE DATA; Schema: public; Owner: myuser
--

COPY public."Todos" ("Id", "Title", "IsCompleted", "Description") FROM stdin;
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: myuser
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20250312200057_Init	9.0.3
\.


--
-- Name: Todos PK_Todos; Type: CONSTRAINT; Schema: public; Owner: myuser
--

ALTER TABLE ONLY public."Todos"
    ADD CONSTRAINT "PK_Todos" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: myuser
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- PostgreSQL database dump complete
--

