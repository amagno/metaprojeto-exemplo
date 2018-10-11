context('Login', () => {
  beforeEach(() => {
    cy.visit('/login')
  })


  it('Login com dados em brancos deve apresentar menssagem de erro', () => {
    cy.get('#login_button').click()
    cy.get('#email_form-helper-text').should('be.visible')
    cy.get('#password_form-helper-text').should('be.visible')
  })

  it('Realizar login com usuário valido', () => {
    // cy.get('#email_form').type('admin@test.com')
    // cy.get('#password_form').type('123')
    // cy.get('#login_button').click()
    cy.login()
    cy.get('h3').first().should('contain', 'Gerênciamento de Projetos')
  })
  it('Realizar login com usuário invalido deve apresentar messagem de erro', () => {
    cy.get('#email_form').type('wrong@test.com')
    cy.get('#password_form').type('123')
    cy.get('#login_button').click()
    cy.contains('p', 'Login inválido!').should('be.visible')
  })
})