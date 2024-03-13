export function validateEmail(email) {
  // Expressão regular para validar o formato do e-mail
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailRegex.test(email);
}
