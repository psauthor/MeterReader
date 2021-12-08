export default {
  isRequired(value) {
    if (value && value.trim()) {
      return true;
    }
    return 'This is required';
  }
}