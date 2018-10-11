context('Project Management', () => {
  it('Testa adiciona projeto com data valida', () => {
    // custom command
    cy.login()
    cy.server()
    cy.route('/api/project-management').as('projects')

    cy.wait('@projects')
    cy.get('input[name="title"]').type('testing')
    cy.get('input[name="startDate"]').type('10102020')
    cy.get('input[name="finishDate"]').type('10102020')
    

    // cy.contains('button', 'Criar').click();
  })
})