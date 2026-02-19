--
-- PostgreSQL database cluster dump
--

-- Started on 2026-02-19 12:08:40 +05

\restrict gg7ajJymgttWgOaLTcdky5NzrS0fPN5KCe8wVZ49IqZBgDo00MEsb189RtN9Cys

SET default_transaction_read_only = off;

SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;

--
-- Roles
--

CREATE ROLE postgres;
ALTER ROLE postgres WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN REPLICATION BYPASSRLS;

--
-- User Configurations
--








\unrestrict gg7ajJymgttWgOaLTcdky5NzrS0fPN5KCe8wVZ49IqZBgDo00MEsb189RtN9Cys

--
-- Databases
--

--
-- Database "template1" dump
--

\connect template1

--
-- PostgreSQL database dump
--

\restrict YXOIxIfZ3Zx6tHQIYK2GTRcjxkfsu399jgFfqd4X6UlAKF626UViV1Yj3iFtcde

-- Dumped from database version 16.10 (Ubuntu 16.10-0ubuntu0.24.04.1)
-- Dumped by pg_dump version 16.10 (Ubuntu 16.10-0ubuntu0.24.04.1)

-- Started on 2026-02-19 12:08:40 +05

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

-- Completed on 2026-02-19 12:08:40 +05

--
-- PostgreSQL database dump complete
--

\unrestrict YXOIxIfZ3Zx6tHQIYK2GTRcjxkfsu399jgFfqd4X6UlAKF626UViV1Yj3iFtcde

--
-- Database "NotesLD" dump
--

--
-- PostgreSQL database dump
--

\restrict PmTuAXswJd2bZ9ybnHjZVwjaPM4g9LSycTul9InOE4kQ9cMYpGAZoSRZFfG5r8o

-- Dumped from database version 16.10 (Ubuntu 16.10-0ubuntu0.24.04.1)
-- Dumped by pg_dump version 16.10 (Ubuntu 16.10-0ubuntu0.24.04.1)

-- Started on 2026-02-19 12:08:40 +05

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

--
-- TOC entry 3404 (class 1262 OID 40960)
-- Name: NotesLD; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "NotesLD" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'C.UTF-8';


ALTER DATABASE "NotesLD" OWNER TO postgres;

\unrestrict PmTuAXswJd2bZ9ybnHjZVwjaPM4g9LSycTul9InOE4kQ9cMYpGAZoSRZFfG5r8o
\connect "NotesLD"
\restrict PmTuAXswJd2bZ9ybnHjZVwjaPM4g9LSycTul9InOE4kQ9cMYpGAZoSRZFfG5r8o

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
-- TOC entry 216 (class 1259 OID 40977)
-- Name: note; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.note (
    id integer NOT NULL,
    title character varying(100),
    text text
);


ALTER TABLE public.note OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 40976)
-- Name: note_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.note_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.note_id_seq OWNER TO postgres;

--
-- TOC entry 3405 (class 0 OID 0)
-- Dependencies: 215
-- Name: note_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.note_id_seq OWNED BY public.note.id;


--
-- TOC entry 3251 (class 2604 OID 40980)
-- Name: note id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.note ALTER COLUMN id SET DEFAULT nextval('public.note_id_seq'::regclass);


--
-- TOC entry 3398 (class 0 OID 40977)
-- Dependencies: 216
-- Data for Name: note; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.note (id, title, text) FROM stdin;
107	ТЕСТ	ДА
\.


--
-- TOC entry 3406 (class 0 OID 0)
-- Dependencies: 215
-- Name: note_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.note_id_seq', 139, true);


--
-- TOC entry 3253 (class 2606 OID 40982)
-- Name: note note_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.note
    ADD CONSTRAINT note_pk PRIMARY KEY (id);


-- Completed on 2026-02-19 12:08:40 +05

--
-- PostgreSQL database dump complete
--

\unrestrict PmTuAXswJd2bZ9ybnHjZVwjaPM4g9LSycTul9InOE4kQ9cMYpGAZoSRZFfG5r8o

--
-- Database "PCLD" dump
--

--
-- PostgreSQL database dump
--

\restrict R3fpIjjy1iQQrSuuYQWNRY6VG3uxsNKLmQe7u3WUPNCtLOQ7hI3Ab9XHVWxKTl6

-- Dumped from database version 16.10 (Ubuntu 16.10-0ubuntu0.24.04.1)
-- Dumped by pg_dump version 16.10 (Ubuntu 16.10-0ubuntu0.24.04.1)

-- Started on 2026-02-19 12:08:40 +05

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

--
-- TOC entry 3475 (class 1262 OID 24582)
-- Name: PCLD; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "PCLD" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'ru_RU.UTF-8';


ALTER DATABASE "PCLD" OWNER TO postgres;

\unrestrict R3fpIjjy1iQQrSuuYQWNRY6VG3uxsNKLmQe7u3WUPNCtLOQ7hI3Ab9XHVWxKTl6
\connect "PCLD"
\restrict R3fpIjjy1iQQrSuuYQWNRY6VG3uxsNKLmQe7u3WUPNCtLOQ7hI3Ab9XHVWxKTl6

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
-- TOC entry 215 (class 1259 OID 24583)
-- Name: _order; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public._order (
    id integer NOT NULL,
    id_client integer,
    id_worker integer,
    date date,
    failure character varying,
    id_object integer,
    price character varying(20),
    clients_words character varying,
    status character varying(40),
    id_order integer
);


ALTER TABLE public._order OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 24588)
-- Name: client; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.client (
    id integer NOT NULL,
    id_human integer
);


ALTER TABLE public.client OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 24591)
-- Name: client_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.client_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.client_id_seq OWNER TO postgres;

--
-- TOC entry 3476 (class 0 OID 0)
-- Dependencies: 217
-- Name: client_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.client_id_seq OWNED BY public.client.id;


--
-- TOC entry 218 (class 1259 OID 24592)
-- Name: human; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.human (
    id integer NOT NULL,
    name character varying(20),
    phone_number character varying(15)
);


ALTER TABLE public.human OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 24595)
-- Name: human_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.human_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.human_id_seq OWNER TO postgres;

--
-- TOC entry 3477 (class 0 OID 0)
-- Dependencies: 219
-- Name: human_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.human_id_seq OWNED BY public.human.id;


--
-- TOC entry 220 (class 1259 OID 24596)
-- Name: object; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.object (
    id integer NOT NULL,
    name character varying(50)
);


ALTER TABLE public.object OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 24599)
-- Name: object for repair_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."object for repair_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."object for repair_id_seq" OWNER TO postgres;

--
-- TOC entry 3478 (class 0 OID 0)
-- Dependencies: 221
-- Name: object for repair_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."object for repair_id_seq" OWNED BY public.object.id;


--
-- TOC entry 222 (class 1259 OID 24600)
-- Name: order_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.order_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.order_id_seq OWNER TO postgres;

--
-- TOC entry 3479 (class 0 OID 0)
-- Dependencies: 222
-- Name: order_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.order_id_seq OWNED BY public._order.id;


--
-- TOC entry 223 (class 1259 OID 24601)
-- Name: users_client; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users_client (
    id integer NOT NULL,
    id_client integer,
    name character varying(50),
    password character varying(50)
);


ALTER TABLE public.users_client OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 24604)
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.users_id_seq OWNER TO postgres;

--
-- TOC entry 3480 (class 0 OID 0)
-- Dependencies: 224
-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_id_seq OWNED BY public.users_client.id;


--
-- TOC entry 225 (class 1259 OID 24605)
-- Name: users_admin; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users_admin (
    id integer DEFAULT nextval('public.users_id_seq'::regclass) NOT NULL,
    id_admin integer,
    name character varying(50),
    password character varying(50)
);


ALTER TABLE public.users_admin OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 24609)
-- Name: users_worker; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users_worker (
    id integer DEFAULT nextval('public.users_id_seq'::regclass) NOT NULL,
    id_worker integer,
    name character varying(50),
    password character varying(50)
);


ALTER TABLE public.users_worker OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 24613)
-- Name: worker; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.worker (
    id integer NOT NULL,
    id_human integer
);


ALTER TABLE public.worker OWNER TO postgres;

--
-- TOC entry 228 (class 1259 OID 24616)
-- Name: worker_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.worker_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.worker_id_seq OWNER TO postgres;

--
-- TOC entry 3481 (class 0 OID 0)
-- Dependencies: 228
-- Name: worker_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.worker_id_seq OWNED BY public.worker.id;


--
-- TOC entry 3284 (class 2604 OID 24617)
-- Name: _order id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public._order ALTER COLUMN id SET DEFAULT nextval('public.order_id_seq'::regclass);


--
-- TOC entry 3285 (class 2604 OID 24618)
-- Name: client id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client ALTER COLUMN id SET DEFAULT nextval('public.client_id_seq'::regclass);


--
-- TOC entry 3286 (class 2604 OID 24619)
-- Name: human id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.human ALTER COLUMN id SET DEFAULT nextval('public.human_id_seq'::regclass);


--
-- TOC entry 3287 (class 2604 OID 24620)
-- Name: object id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.object ALTER COLUMN id SET DEFAULT nextval('public."object for repair_id_seq"'::regclass);


--
-- TOC entry 3288 (class 2604 OID 24621)
-- Name: users_client id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users_client ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);


--
-- TOC entry 3291 (class 2604 OID 24622)
-- Name: worker id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker ALTER COLUMN id SET DEFAULT nextval('public.worker_id_seq'::regclass);


--
-- TOC entry 3456 (class 0 OID 24583)
-- Dependencies: 215
-- Data for Name: _order; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public._order (id, id_client, id_worker, date, failure, id_object, price, clients_words, status, id_order) FROM stdin;
3	1	1	2025-04-11	охлаждение	1	500	видеокарта нагревается	ремонт	1
5	1	1	2025-04-13	охлаждение	1	500	видеокарта нагревается	выдано	1
4	1	1	2025-04-11	охлаждение	1	500	видеокарта нагревается	готова к выдаче	1
1	1	1	2025-04-11	-	1	-	видеокарта нагревается	новый	1
2	1	1	2025-04-11	-	1	-	видеокарта нагревается	диагностика	1
6	3	1	2025-05-01	-	3	-	-	новый	6
7	3	1	2025-05-01	-	3	-	-	диагностика	6
8	3	1	2025-05-01	-	3	-	-	не подлежит ремонту	6
9	3	1	2025-05-02	-	3	-	-	выдано	6
18	1	1	2025-11-23	-	12	-	-	новый	18
19	4	1	2025-11-04	-	10	-	-	новый	19
43	1	1	2025-11-23	-	12	-	-	диагностика	18
44	4	1	2025-11-23	-	10	-	-	диагностика	19
45	1	1	2025-11-24	-	12	-	-	не подлежит ремонту	18
46	1	1	2025-11-24	-	12	-	-	готова к выдаче	18
47	1	1	2025-11-25	-	12	-	-	выдано	18
48	4	1	2025-11-25	Гряз	10	100	-	ремонт	19
49	4	1	2025-11-26	Гряз	10	100	-	готова к выдаче	19
50	4	1	2025-11-27	Гряз	10	100	-	выдано	19
\.


--
-- TOC entry 3457 (class 0 OID 24588)
-- Dependencies: 216
-- Data for Name: client; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.client (id, id_human) FROM stdin;
1	2
3	3
4	6
5	8
\.


--
-- TOC entry 3459 (class 0 OID 24592)
-- Dependencies: 218
-- Data for Name: human; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.human (id, name, phone_number) FROM stdin;
1	ЛЛЛ	+48586424554
2	HHH	+15646545573
3	МММ	+48464876574
4	LicenDrinc	none
6	T T T	+11111
7	Р Р Р	+122222
8	YYY	+84154684
\.


--
-- TOC entry 3461 (class 0 OID 24596)
-- Dependencies: 220
-- Data for Name: object; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.object (id, name) FROM stdin;
1	Nvidia GeForce RTX 3060 12 GB
2	Nvidia GeForce RTX 2060 6 GB
4	Nvidia GeForce RTX 4050 6 GB
6	Nvidia GeForce RTX 4060 Ti 16 GB
9	Nvidia GeForce RTX 5090 32 GB
3	Nvidia GeForce RTX 2070 8 GB
11	Nvidia GeForce RTX 2060 SUPER 8 GB
12	Nvidia GeForce RTX 2070 SUPER 8 GB
13	Nvidia GeForce RTX 2080 8 GB
14	Nvidia GeForce RTX 2080 SUPER 8 GB
15	Nvidia GeForce RTX 2080 Ti 12 GB
16	Nvidia GeForce RTX 3050 6 GB
17	Nvidia GeForce RTX 3060 Ti 8 GB
18	Nvidia GeForce RTX 3070 8 GB
19	Nvidia GeForce RTX 3070 Ti 8 GB
20	Nvidia GeForce RTX 3080 10 GB
21	Nvidia GeForce RTX 3080 Ti 12 GB
22	Nvidia GeForce RTX 3090 24 GB
23	Nvidia GeForce RTX 3090 Ti 24 GB
24	Nvidia GeForce RTX 4070 12 GB
25	Nvidia GeForce RTX 4070 SUPER 12 GB
26	Nvidia GeForce RTX 4070 Ti 12 GB
27	Nvidia GeForce RTX 4070 Ti SUPER 16 GB
28	Nvidia GeForce RTX 4080 16 GB
29	Nvidia GeForce RTX 4080 SUPER 16 GB
30	Nvidia GeForce RTX 4090 D 24 GB
31	Nvidia GeForce RTX 4090 24 GB
32	Nvidia GeForce RTX 5050 8 GB
33	Nvidia GeForce RTX 5060 8 GB
35	Nvidia GeForce RTX 5070 12 GB
36	Nvidia GeForce RTX 5070 Ti 16 GB
37	Nvidia GeForce RTX 5080 16 GB
10	Nvidia GeForce RTX 4060 8 GB
34	Nvidia GeForce RTX 5060 Ti 16 GB
38	Nvidia GeForce RTX 5090 D 48 GB
39	Nvidia GeForce RTX 5060 Ti 8 GB
40	Nvidia GeForce RTX 3060 8 GB
41	Nvidia GeForce RTX 4060 Ti 8 GB
42	Intel Core i5-11400F
\.


--
-- TOC entry 3466 (class 0 OID 24605)
-- Dependencies: 225
-- Data for Name: users_admin; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users_admin (id, id_admin, name, password) FROM stdin;
1	4	LicenDrinc	Lick
\.


--
-- TOC entry 3464 (class 0 OID 24601)
-- Dependencies: 223
-- Data for Name: users_client; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users_client (id, id_client, name, password) FROM stdin;
4	4	LicenDrinc	aaaa
5	2	test2	2
6	3	test3	3
\.


--
-- TOC entry 3467 (class 0 OID 24609)
-- Dependencies: 226
-- Data for Name: users_worker; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users_worker (id, id_worker, name, password) FROM stdin;
2	4	LicenDrinc	kciL
3	1	test1	1
8	3	test2	111
9	4	test3	12
\.


--
-- TOC entry 3468 (class 0 OID 24613)
-- Dependencies: 227
-- Data for Name: worker; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.worker (id, id_human) FROM stdin;
1	1
2	4
3	6
4	7
\.


--
-- TOC entry 3482 (class 0 OID 0)
-- Dependencies: 217
-- Name: client_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.client_id_seq', 5, true);


--
-- TOC entry 3483 (class 0 OID 0)
-- Dependencies: 219
-- Name: human_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.human_id_seq', 8, true);


--
-- TOC entry 3484 (class 0 OID 0)
-- Dependencies: 221
-- Name: object for repair_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."object for repair_id_seq"', 42, true);


--
-- TOC entry 3485 (class 0 OID 0)
-- Dependencies: 222
-- Name: order_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.order_id_seq', 50, true);


--
-- TOC entry 3486 (class 0 OID 0)
-- Dependencies: 224
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_id_seq', 9, true);


--
-- TOC entry 3487 (class 0 OID 0)
-- Dependencies: 228
-- Name: worker_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.worker_id_seq', 4, true);


--
-- TOC entry 3295 (class 2606 OID 24624)
-- Name: client client_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_pk PRIMARY KEY (id);


--
-- TOC entry 3297 (class 2606 OID 24626)
-- Name: human human_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.human
    ADD CONSTRAINT human_pk PRIMARY KEY (id);


--
-- TOC entry 3299 (class 2606 OID 24628)
-- Name: object object_for_repair_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.object
    ADD CONSTRAINT object_for_repair_pk PRIMARY KEY (id);


--
-- TOC entry 3293 (class 2606 OID 24630)
-- Name: _order order_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public._order
    ADD CONSTRAINT order_pk PRIMARY KEY (id);


--
-- TOC entry 3303 (class 2606 OID 24632)
-- Name: users_admin users_admin_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users_admin
    ADD CONSTRAINT users_admin_pk PRIMARY KEY (id);


--
-- TOC entry 3301 (class 2606 OID 24634)
-- Name: users_client users_client_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users_client
    ADD CONSTRAINT users_client_pk PRIMARY KEY (id);


--
-- TOC entry 3305 (class 2606 OID 24636)
-- Name: users_worker users_worker_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users_worker
    ADD CONSTRAINT users_worker_pk PRIMARY KEY (id);


--
-- TOC entry 3307 (class 2606 OID 24638)
-- Name: worker worker_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_pk PRIMARY KEY (id);


--
-- TOC entry 3311 (class 2606 OID 24639)
-- Name: client client_human_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_human_fk FOREIGN KEY (id_human) REFERENCES public.human(id);


--
-- TOC entry 3308 (class 2606 OID 24644)
-- Name: _order order_client_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public._order
    ADD CONSTRAINT order_client_fk FOREIGN KEY (id_client) REFERENCES public.client(id);


--
-- TOC entry 3309 (class 2606 OID 24649)
-- Name: _order order_object_for_repair_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public._order
    ADD CONSTRAINT order_object_for_repair_fk FOREIGN KEY (id_object) REFERENCES public.object(id);


--
-- TOC entry 3310 (class 2606 OID 24654)
-- Name: _order order_worker_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public._order
    ADD CONSTRAINT order_worker_fk FOREIGN KEY (id_worker) REFERENCES public.worker(id);


--
-- TOC entry 3312 (class 2606 OID 24659)
-- Name: worker worker_human_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_human_fk FOREIGN KEY (id_human) REFERENCES public.human(id);


-- Completed on 2026-02-19 12:08:41 +05

--
-- PostgreSQL database dump complete
--

\unrestrict R3fpIjjy1iQQrSuuYQWNRY6VG3uxsNKLmQe7u3WUPNCtLOQ7hI3Ab9XHVWxKTl6

-- Completed on 2026-02-19 12:08:41 +05

--
-- PostgreSQL database cluster dump complete
--

