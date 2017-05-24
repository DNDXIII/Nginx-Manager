FROM ubuntu:16.04

RUN \
    apt-get update && \
    apt-get -yqq install ssh && \
    apt-get install -y git && \
    mkdir -p /root/.ssh/ && \
    ssh-keyscan gitlab.com >> /root/.ssh/known_hosts && \
    echo '-----BEGIN RSA PRIVATE KEY-----\r\nMIIEpQIBAAKCAQEAxG1H8h7xWsKkuk3xTZ5oojbZo+65KPfP2pvNgMSlntAm2Stv\r\nrux9AlCJwBCawBCGlLvUT9F9rdlhIyasOQWQXpSN1VFZ0Itn5z9LMzStjuvSjAyx\r\n2Y7BVyJ2uw1c5IzXBVD/21qhs32iz+tRAC3oMGzFUVF07R1lsJfSimYTCQEL+2cm\r\nZwZar5BIGaOKQlSYj2FU2QFL0hUhlLojC1RUIjtEgS9qxrjChTdmsdDQ3HztHDzb\r\nrEczm4StUJk2XjgDBPT+rUYDSjYPvGvxqXwc2GgVqD4CkBNVPA9V1eX20yKZtRQr\r\nL4VuiL5WG/eGtWl4FR2LVsP7T2lHCYxYJlEk9wIDAQABAoIBAF15L5qNlQc+xwsN\r\nWj44msxO08UKLjzLqLL8H5AehD4OCd6gp/YS/e4jC1pFzI6HjrOqqn8NTwRzBKGo\r\nnp5IAoqvoMsZemceX+SQnyWP9OOlXz1cMSo5n9HqSZv74O+2AgC6qfblqztyFw9g\r\niNxP/EVHDaMD42OO8LxXk05fAsHuUx3eFscFux3GcxMzOYSOyfT5hL+kY8w6NtbW\r\nljHkggpAbmaFVDvxut7F2ep1XmnrRamtF6BhD8WVJNMYFN7qqe7ctMyLzKiTlnhd\r\nkLDYfZM25aiG1wetc3bKL88qEqViKv53Sd7U0N9Kum3nFf/RcsvRGPO6XWycQrG5\r\nzrm5mSkCgYEA5VoXyTtSWGKwPFH+6YBqOiNSarDaC5wOYY4Pbgu/PUZPxCh/UYAO\r\nQVVoe+HJ7ZhiDoSlFlqCEbM1HF17BK8eF1jc9kDrAJlgU8pBqyixSFSpvDgYAoLs\r\n0UPMP0GeYzH1Kr/JOZAhsjuONQY8kG0CGM3eRmAVQvujr4tI8vwFPGMCgYEA2z/b\r\nP60uPciSx7nEpt3OPfvePYFQPZ7Kvryjfc7w4tO5U0pEhxiRLssdxWdZYnzrPR0w\r\nYbJUa20XzVzWLC0SzhWVu7Il28Vcyf29KKD5/jVQsUIDJnzdZpsW9luXot5/MhhE\r\nXbQeZn8mBMKdUmAU79hXol6/dLz92cTm46vGh10CgYEA3ummrrYSfcdFhQso2Cgw\r\nltsX0oybEKeXrqeVRZ7zO4noIz49KKhusn9hcu5sBZqSy4uOrOBBBRUJrM0KsO3H\r\nMGf1Mv67qbRo1GeDVejSDfwDkOVwKJi5tVbQ4DZVzBGVOPx4hpMYvRN3TwAFw1W/\r\nsP3+aKYgZB8R459bwpbCPAMCgYEAy7JQKKDlTVGDzTuykLWSX9QZblsuCZH2G692\r\n1fPlqVe73r4pXLpeaHMdfcG/MdmNVIJB6QKdnBEHBC00bmaSChXTv0aMFdsPkjrD\r\n+3NTz/IIvibudlub9zAv7UK7Zv4AfrI9EjG97MxErjeBon7R9OOyx4/voK3VyvU8\r\n7lkxYaECgYEAvVUo2fEXhnDwQ3WVh/+ElCEPRspEwcJ7UA6iozoYCgHF+Xe3i0+/\r\nwg/JUN3idEHUTAVXVMa5dqwEPe1ZYE6+R4OEP5J80EfEWUbbsc4vG96aQ6svfDP9\r\nVGuJltxbCcNGiAKsqrFjRjo/0Y0jXGFFyE83hcdYUVfka3UZu+fouUY=\r\n-----END RSA PRIVATE KEY-----' > /root/.ssh/id_rsa && \
    chmod 400 /root/.ssh/id_rsa && \
    git clone git@gitlab.com:collab-azure/nginx-redundant-floatingip-ubuntu.git && \
    cd nginx-redundant-floatingip-ubuntu/debian && \
    chmod +x *.sh && \
    ./setup_nginx.sh