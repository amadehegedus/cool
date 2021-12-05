// jest.config.js
module.exports = {
  preset: 'jest-preset-angular',
  setupFilesAfterEnv: ['<rootDir>/setup-jest.ts'],
  moduleNameMapper: {
    "../../api/app.generated": "<rootDir>/src/app/api/app.generated.ts",
  }
};
